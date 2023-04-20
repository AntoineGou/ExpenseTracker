# Readme

to run everything
dotnet tool restore && dotnet cake --target=All

prereq : dotnet, docker

depending on your computer the cake build might fail on db initialization (wait 15 sec might be too short for db to startup) just rerun the script

