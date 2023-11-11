contained: run
	cd ./gen && dotnet publish -r linux-x64 --sc Main.fsproj
	cp gen/bin/Debug/net7.0/linux-x64/publish/Main .
exec: run
	cd ./gen && dotnet run Main.fsproj

build:
	dotnet build
clean:
	dotnet clean
	rm -rf ./bin/ ./obj/
	rm -rf ./gen/bin/ ./gen/obj/
	rm ./Main
run:
	dotnet run
watch:
	dotnet watch build

