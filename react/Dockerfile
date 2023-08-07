FROM node:16-alpine as build

WORKDIR /app
COPY . .

COPY package*.json ./
RUN npm install && npm run build

FROM nginx:alpine

COPY --from=build /app/build /usr/share/nginx/html

EXPOSE 3000
CMD ["/bin/sh", "-c", "nginx -g \"daemon off;\""]