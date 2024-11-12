#! /usr/bin/sh

docker-compose up -d

run_migrations() {
  dotnet ef database update --no-build > /dev/null 2>&1
}

while true; do
	if run_migrations; then
		echo Conectado e rodando Migrations...
		break
	fi
	echo Tentando conectar...
done

dotnet run