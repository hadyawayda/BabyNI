# This a tutorial for the well established SaaS "Network Insight" by Yuvo.

## Contents
The project contains 2 iterations, demonstrating the progress of my knowledge from the start of the internship until the day of submission.
The first iteration is a console app (class library projetct) found under the "main" branch, the second iteration is a microservices (ASP.NET Core Web API) implementation found under the "staging" branch.
The UI is found under the "staging" branch, and is the same for both iterations.
The API Controller is found under the "staging" branch, and is usable by both iterations as well.

## Requirements
To replicate the first iteration (console app), install node 18 and .Net 7.0, and start by cloning the code.
To replicate the second iteration, install all the above, and if you need to run all the docker containers, docker is required.

## Instructions
1-  To run the first iteration, open BabyNI.sln from the "main" branch in Visual Studio, which is a console app containing the watcher, parser, loader and aggregator.
Right click on the solution in file explorer and click build solution, and then run the BabyNI.exe build file found in BabyNI/bin/debug/win64.
The API Controller is found under the "staging" branch, and can be started by running the BabyAPI.exe build file found in BabyAPI/bin/debug/win64.
The React and NextJs app is found under the "staging" branch as well, and can be started by opening the UI folder in Visual Studio Code, then running npm install in the console (ctrl + `), and then by running npm run dev and opening the http://localhost:3000 url in the browser.

2- To run the second iteration, open BabyNI.sln from the "staging" branch in Visual Studio, and build the projects (ctrl + shift+ b), then execute them either one by one (parser then loader then aggregator then BabyAPI) or modify the properties of the solution to start all the projects at the same time, or run their executables from their respective /bin/debug/win64 directories. Note that for these APIs to work in windows, you need to comment out each <RuntimeIdentifier>linux-x64</RuntimeIdentifier> line the .csproj file for each project.

3- To run the second iteration, with support for docker and containerization, clone the "staging" branch, and follow the instructions in the readme.md file under the "staging" branch.
