FROM node:alpine as app-build
RUN mkdir -p /app
WORKDIR /app
COPY package.json package-lock.json ./app/
COPY . /app
RUN npm install
RUN npx nx build UI --configuration=production

FROM nginx:1.17.1-alpine
COPY --from=app-build /app/nginx.conf /etc/nginx/nginx.conf
COPY --from=app-build /app/dist/UI/browser /usr/share/nginx/html