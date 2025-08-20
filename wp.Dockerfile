FROM wordpress:6.8.0

RUN apt-get update
RUN apt-get install -y imagemagick
