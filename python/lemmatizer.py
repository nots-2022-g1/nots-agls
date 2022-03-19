#!/usr/bin/env python
import pika
from nltk import WordNetLemmatizer

lemmatizer = WordNetLemmatizer()

connection = pika.BlockingConnection(
    pika.ConnectionParameters(host='localhost'))

channel = connection.channel()


def on_request(ch, method, props, body):
    response = lemmatizer.lemmatize(body)
    print(f'{body} -> {response}')

    ch.basic_publish(exchange='',
                     routing_key=props.reply_to,
                     properties=pika.BasicProperties(correlation_id= \
                                                         props.correlation_id),
                     body=str(response))
    ch.basic_ack(delivery_tag=method.delivery_tag)


channel.basic_qos(prefetch_count=1)
channel.basic_consume(queue='get-lemmatized-word', on_message_callback=on_request)

print(" [x] Awaiting RPC requests")
channel.start_consuming()
