# ECommerceBackend

1- Redis ve RabbitMq uygulamalarının kurulması gerekmektedir.
   - docker-compose.yml dosyası "docker-compose up -d" komutu çalıştırılarak docker uygulaması üzerinden image ve container'lar oluşturulmalı.
   - Api ve worker uygulamalarının appsettings ayarları rabbitmq ve redis ayarları için yapılmalı.
     
2- Api'de jwt token için issuer, audience url ayarları yapılmalı.

3- Api'de appsettings ayarlarında MSSQLServer için default connection ayarı yapılmalı.

4- Api url'i değişirse postman collectiondaki requestlerde default url bulunmakta değiştirilmelidir.

Rabbitmq ve Redis uygulamaları ayağa kaldırıldıktan sonra api https olarak çalıştırılmalı ve arkasından worker servisinin debug modda ayağa kaldırılması gerekmektedir.
