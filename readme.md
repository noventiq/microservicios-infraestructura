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

2. Levantar la infraestructura ubicandose en el folder `infraestructura`. Abrir una consola de tipo SSH y ejecutar desde la consola el comando:

    ```
    docker-compose up
    ```

    Esto leerá la configuración del archivo `docker-compose.yml`

3. Levantar la interfaz de usuario de kafka. Cambiar la ruta de acuerdo a la ubicación de > Java 17 de acuerdo a la ubicación dónde se instaló

    ```
    C:\jvms\store\17.0.1\bin\java.exe -jar E:\projects\ANA\Cursos\Microservicios\code\infraestructura\ui\kafka-ui-api-v0.6.1.jar --spring.config.additional-location=E:\projects\ANA\Cursos\Microservicios\code\infraestructura\ui\application-local.yml
    ```

    Luego ingresar a la interfaz de usuario de Kafka: http://localhost:8080/

    Luego para ver los datos de elastic search: http://localhost:5601/

4. Asegurarse de tener restaurado la base de datos `BD_NET_BACKEND` en SQL Server. Lo puede descargar desde el siguiente enlace https://softlineholdingplc-my.sharepoint.com/:f:/g/personal/isaias_mayon_noventiq_com/Em4icnU5F-9Iq2plktzTvMkBGTi7iLu7_zmaDuYPhtpGbA?e=oRAF8Q 

5. Crear el tópico `sqlserver_products` desde `Kafka UI` desde el contenedor de kafka

    ```
    docker exec -it broker /bin/bash
    ```
    ```
    cd /usr/bin
    ```
    ```
    kafka-topics --bootstrap-server broker:9092 --create --topic sqlserver_products --partitions 1 --replication-factor 1
    ```

6.  Librerías para Kafka connect

    6.1 Kafka connect JDBC

    https://www.confluent.io/hub/confluentinc/kafka-connect-jdbc

    6.2 SQL Server JDBC
    
    https://learn.microsoft.com/en-us/sql/connect/jdbc/download-microsoft-jdbc-driver-for-sql-server?view=sql-server-ver16

    6.3 Kafka connect Mongodb 
    
    https://www.confluent.io/hub/mongodb/kafka-connect-mongodb

    6.4 Kafka connect Elasticsearch
    
    https://www.confluent.io/hub/confluentinc/kafka-connect-elasticsearch

7. Creación de conectores source y sink

    7.1 Crear Source connector ODBC SQL Server 

    ```
    curl --location 'http://localhost:8083/connectors/' \
    --header 'Content-Type: application/json' \
    --header 'Accept: application/json' \
    --data '{
        "name": "products-sqlserver-jdbc-source-connector",
        "config": {
            "connector.class": "io.confluent.connect.jdbc.JdbcSourceConnector",
            "tasks.max": 1,
            "connection.url": "jdbc:sqlserver://sqlserver2022:1433;databaseName=BD_NET_BACKEND;user=sa;password=PasswordO1.",
            "query": "select * from (select * from products where id <10) as tbl",
            "mode": "incrementing",
            "incrementing.column.name": "id",
            "topic.prefix": "sqlserver_products",
            "validate.non.null": false,
            "transforms":"copyFieldToKey,extractKeyFromStruct",
            "transforms.copyFieldToKey.type":"org.apache.kafka.connect.transforms.ValueToKey",
            "transforms.copyFieldToKey.fields":"id",
            "transforms.extractKeyFromStruct.type":"org.apache.kafka.connect.transforms.ExtractField$Key",
            "transforms.extractKeyFromStruct.field":"id"
        }
    }'
    ```

    7.2 Crear Sink Connector MongoDB
    ```
    curl --location 'http://localhost:8083/connectors/' \
    --header 'Content-Type: application/json' \
    --header 'Accept: application/json' \
    --data-raw '{
        "name": "customers-mongo-sink-connector",
        "config": {
            "connector.class": "com.mongodb.kafka.connect.MongoSinkConnector",
            "tasks.max": "1",
            "topics": "sqlserver_products",
            "connection.uri": "mongodb://root:rootpassword@mongo-db:27017",
            "connection.user": "root",
            "connection.password": "rootpassword",
            "database": "products_microservice",
            "collection": "products",
            "key.converter": "org.apache.kafka.connect.storage.StringConverter",
            "key.converter.schemas.enable": true,
            "document.id.strategy":"com.mongodb.kafka.connect.sink.processor.id.strategy.ProvidedInKeyStrategy",
            "transforms":"hk",
            "transforms.hk.type":"org.apache.kafka.connect.transforms.HoistField$Key",
            "transforms.hk.field":"_id"
        }
    }'
    ```
    7.2 Crear Sink Connector Elasticsearch
    ```
    curl --location 'http://localhost:8083/connectors/' \
    --header 'Content-Type: application/json' \
    --header 'Accept: application/json' \
    --data '{
        "name": "products-elasticsearch-sink-connector",
        "config": {
            "connector.class": "io.confluent.connect.elasticsearch.ElasticsearchSinkConnector",
            "tasks.max": "1",
            "topics": "sqlserver_products",
            "key.ignore": "true",
            "schema.ignore": "true",
            "connection.url": "http://elasticsearch:9200",
            "type.name": "_doc",
            "name": "products-elasticsearch-sink-connector",
            "key.converter": "org.apache.kafka.connect.json.JsonConverter",
            "key.converter.schemas.enable": true
        }
    }'
    ```

8. Orden de carga de variables en .NET en el uso de contenedores

    1. appsetting.json
    2. appsettings..json
    3. secret.json
    4. environment variables
    5. cli args

    <br />
    Ejemplo de uso del archivo  `appSettings.json`

    ```json
    {
        "Logging": {
            "LogLevel": {
            "Default": "Information",
            "Microsoft.AspNetCore": "Warning"
            }
        },
        "AllowedHosts": "*",
        "SqlServerSettings": {
            "ConnectionString": "Server=.;Database=BD_NET_BACKEND;User Id=sa;Password=PasswordO1.;"
        },
        "MongoDatabaseSettings": {
            "ConnectionString": "mongodb://root:rootpassword@localhost:27017/admin"
        }
    }
    ```

    Declaración de parámetros en Dockerfile

    ```Docker
    ENV Logging__LogLevel__Default=Trace

    ENV SqlServerSettings__ConnectionString=Server=.;Database=BD_NET_BACKEND;User Id=sa;Password=PasswordO1.;

    ENV MongoDatabaseSettings__ConnectionString=mongodb://root:rootpassword@localhost:27017/admin;
    ```

    Construcción de imagen
    ```Docker
    docker build -t user_microservice:1.0 . 
    ```

    Creación de contenedor basado en imagen
    ```Docker
    docker run -d --name xxxxx1 -e "SqlServerSettings__ConnectionString=Server=rogwin11;Database=BD_NET_BACKEND;User Id=sa;Password=PasswordO1." -e "MongoDatabaseSettings__ConnectionString=mongodb://root:rootpassword@rogwin11:27017/admin" -p 5000:80 user_microservice:1.0
    ```