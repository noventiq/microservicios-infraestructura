1. Configuración de equipo
- Habilitar virtualización en el computador desde BIOS
- Java Version Manager: https://github.com/ystyle/jvms 
  - Java 11
  - Java 17
- WSL: https://learn.microsoft.com/es-es/windows/wsl/install 
- Distribución Linux: https://aka.ms/wslstore 
- Docker desktop: https://www.docker.com/products/docker-desktop/ 
- Minikube: https://minikube.sigs.k8s.io/docs/start/ 
- Confluent / Apache Kafka: https://docs.confluent.io/platform/current/installation/installing_cp/zip-tar.- html#get-the-software 
- KafkaUI: https://github.com/provectus/kafka-ui 

2. Levantar desde la raiz de este proyecto
docker-compose up

3. Levantar la interfaz de usuario de kafka

`C:\jvms\store\17.0.1\bin\java.exe -jar E:\projects\ANA\Cursos\Microservicios\kafka-ui-api-v0.6.1.jar --spring.config.additional-location=E:\projects\ANA\Cursos\Microservicios\application-local.yml`


4. Script de postresql

create table products(
 id int PRIMARY KEY,
 title varchar(500),
 description varchar(1024),
 price decimal(10,2),
 discountpercentage decimal(10,2),
 rating decimal(10,2),
 stock int,
 brand varchar(50),
 category varchar(50),
 createdat timestamp,
 createdby varchar(50),
 updatedat timestamp,
 updatedby varchar(50)
);


INSERT INTO products (id, title, description, price, discountpercentage, rating, stock, brand, category, createdat, createdby, updatedat, updatedby)VALUES (1, N'iPhone 9', N'An apple mobile which is nothing like apple', CAST(549.00 AS Decimal(10, 2)), CAST(12.96 AS Decimal(10, 2)), CAST(4.69 AS Decimal(10, 2)), 94, N'Apple', N'smartphones', now(), 'admin', now(), 'invitado');


INSERT INTO products (id, title, description, price, discountpercentage, rating, stock, brand, category, createdat, createdby, updatedat, updatedby) VALUES (3, N'Samsung Universe 9', N'Samsungs new variant which goes beyond Galaxy to the Universe', CAST(1249.00 AS Decimal(10, 2)), CAST(15.46 AS Decimal(10, 2)), CAST(4.09 AS Decimal(10, 2)), 36, N'Samsung', N'smartphones', now(), 'admin', now(), 'invitado');

INSERT INTO products (id, title, description, price, discountpercentage, rating, stock, brand, category, createdat, createdby, updatedat, updatedby) VALUES (4, N'OPPOF19', N'OPPO F19 is officially announced on April 2021.', CAST(280.00 AS Decimal(10, 2)), CAST(17.91 AS Decimal(10, 2)), CAST(4.30 AS Decimal(10, 2)), 123, N'OPPO', N'smartphones', now(), 'admin', now(), 'invitado');

INSERT INTO products (id, title, description, price, discountpercentage, rating, stock, brand, category, createdat, createdby, updatedat, updatedby) VALUES (5, N'Huawei P30', N'Huawei’s re-badged P30 Pro New Edition was officially unveiled yesterday in Germany and now the device has made its way to the UK.', CAST(499.00 AS Decimal(10, 2)), CAST(10.58 AS Decimal(10, 2)), CAST(4.09 AS Decimal(10, 2)), 32, N'Huawei', N'smartphones', now(), 'admin', now(), 'invitado');

INSERT INTO products (id, title, description, price, discountpercentage, rating, stock, brand, category, createdat, createdby, updatedat, updatedby) VALUES (6, N'MacBook Pro', N'MacBook Pro 2021 with mini-LED display may launch between September, November', CAST(1749.00 AS Decimal(10, 2)), CAST(11.02 AS Decimal(10, 2)), CAST(4.57 AS Decimal(10, 2)), 83, N'Apple', N'laptops', now(), 'admin', now(), 'invitado');

INSERT INTO products (id, title, description, price, discountpercentage, rating, stock, brand, category, createdat, createdby, updatedat, updatedby) VALUES (7, N'Samsung Galaxy Book', N'Samsung Galaxy Book S (2020) Laptop With Intel Lakefield Chip, 8GB of RAM Launched', CAST(1499.00 AS Decimal(10, 2)), CAST(4.15 AS Decimal(10, 2)), CAST(4.25 AS Decimal(10, 2)), 50, N'Samsung', N'laptops', now(), 'admin', now(), 'invitado');

5. Crear el tópico `postgres_products` desde el contenedor de kafka

docker exec -it broker /bin/bash
cd /usr/bin

kafka-topics --bootstrap-server broker:9092 --create --topic postgres_products --partitions 1 --replication-factor 1

6. Librerías para Kafka connect

https://www.confluent.io/hub/confluentinc/kafka-connect-jdbc
https://dev.mysql.com/downloads/file/?id=515796
https://jdbc.postgresql.org/download/
https://www.oracle.com/pe/database/technologies/appdev/jdbc-downloads.html
https://central.sonatype.com/artifact/org.mongodb/mongodb-jdbc/2.0.2/versions?smo=true
https://www.confluent.io/hub/mongodb/kafka-connect-mongodb
https://www.confluent.io/hub/confluentinc/kafka-connect-elasticsearch
