FROM node:alpine as app-build
RUN mkdir -p /app
WORKDIR /app
COPY package.json package-lock.json ./app/
COPY . /app
RUN npm install
RUN ./node_modules/.bin/ng build --prod

FROM nginx:1.17.1-alpine
COPY --from=app-build /app/nginx.conf /etc/nginx/nginx.conf
COPY --from=app-build /app/dist/UI /usr/share/nginx/html