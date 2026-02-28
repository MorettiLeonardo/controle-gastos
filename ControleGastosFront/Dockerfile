FROM node:20-alpine

WORKDIR /app

COPY package*.json ./
RUN npm install

COPY . .

ARG VITE_BASE_URL=http://localhost:5000/api/v1
ENV VITE_BASE_URL=$VITE_BASE_URL
RUN npm run build

RUN npm install -g serve

EXPOSE 3000
CMD ["serve", "-s", "dist", "-l", "3000"]