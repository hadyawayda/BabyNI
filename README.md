# This a tutorial for the well established SaaS "Network Insight" by Yuvo.

## Contents
The project contains 2 iterations, demonstrating the progression of my knowledge from the start of the internship until the day of submission.
The first one is a console app (class library projetct) found under the "main" branch, the second iteration is a microservices implementation (ASP.NET Core Web API) found under the "staging" branch.
The UI is found under the "staging" branch, and is the same for both iterations.
The API Controller is found under the "staging" branch, and is usable by both iterations as well.

## Requirements
To replicate the first iteration (console app), install node 18 and .Net 7.0, and start by cloning the code.
To replicate the second iteration, install all the above, and if you need to run all the containers, docker is required.

## Instructions
1-  To run the first iteration, open BabyNI.sln in Visual Studio, which is a console app containing the watcher, parser, loader and aggregator.
    Right click on the solution in file explorer and click build solution, and then run the BabyNI.exe build file found in BabyNI/bin/debug/win64.
    The API Controller is found under the "staging" branch, and can be started by running the BabyAPI.exe build file found in BabyAPI/bin/debug/win64.
