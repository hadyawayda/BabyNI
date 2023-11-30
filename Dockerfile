# Stage 1: Building the app
FROM node:lts AS builder

# Set the working directory in the container
WORKDIR /app

# Copy package.json and package-lock.json (or yarn.lock)
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of your app's source code
COPY . .

# Build your Next.js application
#RUN npm run build

# Stage 2: Run the app
# FROM node:lts

# WORKDIR /app

# # Copy the built app from the builder stage
# COPY --from=builder /app/public ./public
# COPY --from=builder /app/.next ./.next
# COPY --from=builder /app/node_modules ./node_modules
# COPY --from=builder /app/package.json ./package.json

# Expose the port the app runs on
EXPOSE 3000

# Start the app
# CMD ["npm", "start"]
CMD ["npm", "run", "dev"]
