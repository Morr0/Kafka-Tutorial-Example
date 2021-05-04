```bash
docker run -d --name zookeeper -p 2181:2181 -e ALLOW_ANONYMOUS_LOGIN=yes bitnami/zookeeper:latest

docker run -d --name kafka -p 9092:9092 -e ALLOW_PLAINTEXT_LISTENER=yes \
-e KAFKA_CFG_ZOOKEEPER_CONNECT=127.0.0.1:2181 bitnami/kafka:latest
```