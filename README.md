# BABYNI

## Requirements

- docker and docker compose

## How to run FULL build and run

```sh
docker compose up -d
```

## After any change to rebuild

```sh
docker-compose pull

docker-compose up --build -d
```

## How to build and run single app

- To build the UI for example:
- Go into the project directory

```sh
docker build -t uiimage .

docker run -d --name uicontainer -p 3000:3000 uiimage
```
