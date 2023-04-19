FROM node:lts AS build

# Set the working directory
WORKDIR /src

# Copy the package.json and package-lock.json files and install dependencies
COPY ["package.json", "package-lock.json", "./"]
RUN npm ci

# Copy the rest of the source code
COPY . .

# Build the React TypeScript app
RUN npm run build

# Set the base image as the Nginx stable image
FROM nginx:stable

# Copy the built app to the Nginx HTML directory
COPY --from=build /src/build /usr/share/nginx/html

# Copy the Nginx configuration file (if you have a custom configuration)
# COPY reactapp/nginx.conf /etc/nginx/conf.d/default.conf

# Expose the port that the app is running on (default is 80 for Nginx)
EXPOSE 80

# Set the command to run Nginx
CMD ["nginx", "-g", "daemon off;"]
