FROM wordpress:6.8.2-php8.2-fpm-alpine

RUN apt-get update
RUN apt-get install -y imagemagick
