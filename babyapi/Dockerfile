# Use the official .NET SDK image as the base image for building the application.
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

# Set the working directory inside the container.
WORKDIR /src

# Copy the application source code to the container.
COPY . .

# Restore dependencies.
RUN dotnet restore

# Build and publish the application in Release mode.
RUN dotnet publish -c Release -o /app

# Create a separate stage for the runtime image.
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS runtime

# Set the working directory inside the runtime stage.
WORKDIR /app

# Copy the published application from the build stage to the runtime stage.
COPY --from=build /app .

# Expose the port on which the ASP.NET Core application will listen.
EXPOSE 5000

# Set environment variables for the ASP.NET Core application.
ENV ASPNETCORE_URLS=http://+:5000
ENV ASPNETCORE_ENVIRONMENT=Production

# Define the command to start the application.
CMD ["./BabyAPI"]