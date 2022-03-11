docker-compose up &
cd ./api && alacritty -e dotnet watch &
ls -l
cd ./frontend && alacritty -e dotnet watch &
ls -l

