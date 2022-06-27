#build stage
FROM node:lts-alpine3.14 AS builder

#set working directory
WORKDIR /app

#install app dependencies:

#1. copy package.json to Docker env
COPY package.json ./

#2. install all it's packages
RUN npm install

#3. copy everything to Docker env
COPY . ./

#4. Build release ver
RUN npm run build


# Stage 1, based on Nginx, to have only the compiled app, ready for production with Nginx
FROM nginx:alpine
COPY --from=builder /app/dist/ /usr/share/nginx/html