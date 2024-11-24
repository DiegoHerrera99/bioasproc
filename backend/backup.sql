/*!999999\- enable the sandbox mode */ 
-- MariaDB dump 10.19  Distrib 10.11.8-MariaDB, for debian-linux-gnu (x86_64)
--
-- Host: 127.0.0.1    Database: Bioinsumos
-- ------------------------------------------------------
-- Server version	11.5.2-MariaDB-ubu2404

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `Certifion_Information`
--

DROP TABLE IF EXISTS `Certifion_Information`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Certifion_Information` (
  `certificationInformationId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(128) NOT NULL,
  `body` varchar(255) DEFAULT NULL,
  `modifiedAt` timestamp NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `createdAt` timestamp NULL DEFAULT current_timestamp(),
  `status` tinyint(1) DEFAULT 1,
  PRIMARY KEY (`certificationInformationId`)
) ENGINE=InnoDB AUTO_INCREMENT=10 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Certifion_Information`
--

LOCK TABLES `Certifion_Information` WRITE;
/*!40000 ALTER TABLE `Certifion_Information` DISABLE KEYS */;
INSERT INTO `Certifion_Information` VALUES
(1,'¿Cuál es el enfoque del diplomado en Bioinsumos?','El diplomado en Bioinsumos se enfoca en la producción y uso de insumos biológicos para la agricultura sostenible, promoviendo prácticas agroecológicas en comunidades rurales de Guatemala.','2024-10-18 03:30:08','2024-09-21 19:58:09',0),
(2,'¿Cuáles son los temas principales cubiertos?','Los temas principales incluyen la producción de biofertilizantes, control biológico de plagas, compostaje y técnicas sostenibles de manejo de suelos, aplicadas a la agricultura local.','2024-10-18 03:30:15','2024-09-21 19:58:26',0),
(3,'¿Cuánto tiempo toma completar el diplomado?','El diplomado puede completarse en uno o dos años, dependiendo del ritmo de estudio y la carga de los cursos.','2024-10-18 03:30:19','2024-09-21 19:58:46',0),
(4,'¿Cómo se desarrollan los diplomados?','Los diplomados se imparten de forma presencial en 7 talleres una vez por semana, con una duración de 96 horas teóricas y prácticas.','2024-10-18 03:31:14','2024-10-18 03:31:14',1),
(5,'¿Cuáles son los temas del diplomado?','Los temas por impartir serán sobre la elaboración de bioinsumos; bioinsecticida, biofungicidas, bionematicidas; biofertilizantes, abonos sólidos, manejo y mantenimiento de invernaderos y manejo del cultivo de tomate, pepino y cultivos a campo abierto.','2024-10-18 03:31:43','2024-10-18 03:31:43',1),
(6,'¿Quiénes pueden participar en los diplomados?','La participación en los diplomados es libre y abierto para todo el público, desde agricultores individuales, grupos organizados, asociaciones, cooperativas, empresas privadas y demás.','2024-10-18 03:33:01','2024-10-18 03:32:08',1),
(7,'¿En dónde imparten los diplomados?','Los diplomados se desarrollan en las instalaciones de la Asociación de Productores Comalapenses -ASPROC- y sus áreas productivas.','2024-10-18 03:33:38','2024-10-18 03:33:38',1),
(8,'Sabias que?','Somos el mejor de comalapa','2024-10-29 02:47:09','2024-10-29 02:45:55',0),
(9,'Titulo modificado','Somos el unico diplomado certificado por: XX, XX;','2024-11-01 02:49:04','2024-11-01 02:47:34',0);
/*!40000 ALTER TABLE `Certifion_Information` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Courses`
--

DROP TABLE IF EXISTS `Courses`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Courses` (
  `courseId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(128) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `modifiedAt` timestamp NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `createdAt` timestamp NULL DEFAULT current_timestamp(),
  `status` tinyint(1) DEFAULT 1,
  `imgPath` varchar(128) NOT NULL DEFAULT '',
  PRIMARY KEY (`courseId`)
) ENGINE=InnoDB AUTO_INCREMENT=15 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Courses`
--

LOCK TABLES `Courses` WRITE;
/*!40000 ALTER TABLE `Courses` DISABLE KEYS */;
INSERT INTO `Courses` VALUES
(1,'Curso de prueba','Descripción del curso de prueba','2024-09-04 04:42:42','2024-08-26 02:16:48',0,''),
(2,'Microorganismos de montaña sólidos -MMS-','Son microorganismos de montaña que proliferan hongos, bacterias, micorrizas, levaduras y otros organismos de gran beneficio para la agricultura.','2024-10-13 23:21:48','2024-09-03 21:02:46',1,'a74597d4-3d05-4756-99e7-dfa4b7f0db91.png'),
(3,'Microorganismos de montaña madre líquida -MML-','Fortalece la flora microbiana en terrenos que han sido desgastados por la utilización de químicos.','2024-10-13 23:22:27','2024-09-04 04:24:47',1,'03452492-c0fa-4214-8a8f-2bcb4b325eaa.png'),
(4,'Microorganismos de montaña activados -MMA-','Es la fermentación de microorganismos que se activan al ser enriquecidos con melaza.','2024-10-13 23:22:48','2024-09-04 04:44:28',1,'bb69b59a-c6e5-4e03-9a13-a08e0ec168f2.png'),
(5,'Abono tipo Bocashi','Es un abono orgánico sólido, producto de un proceso de fermentación, que acelera la degradación de la materia orgánica, elevando la temperatura, eliminando patógenos en el proceso.','2024-10-13 23:23:03','2024-09-04 05:00:16',1,'cbc49395-8819-4e49-a233-b70ed28a0581.png'),
(6,'APICHI','Es un bioinsecticida y biofungicida. Controla insectos portadores de enfermedades, hongos, bacterias y otros patógenos como: mosca blanca, trips y pulgones que afectan la producción agrícola.','2024-10-13 23:23:24','2024-09-04 05:19:52',1,'715bde08-0565-481f-ad55-edc979dc3a40.png'),
(7,'string','string','2024-09-04 18:33:26','2024-09-04 18:27:05',0,''),
(8,'M5 - Bioinsecticida, biofungicida y biobactericida.','Es un controlador de insectos su composición permite repeler los insectos que proliferan e infectan el cultivo, como mosca blanca, trips, picudo y pulgones.','2024-10-13 23:23:42','2024-09-04 18:27:05',1,'a839ac8d-1ed9-433e-b256-a085adc8b951.png'),
(9,'7 Caldos','Bionematicida, biofungicida, biobactericida y abono foliar.','2024-10-13 23:23:59','2024-09-04 20:12:04',1,'b6d60974-21d5-4b95-97b6-33ced7315365.png'),
(10,'AC-ASPROC','Funciona como biofungicida y biobactericida; si se emplea alcohol actúa como nematicida.','2024-10-13 23:24:22','2024-09-04 20:36:23',1,'9d761652-b9f7-4e2f-ae64-c51a48610b22.png'),
(11,'Caldo sulfocálcico','Es un bioinsecticida, biofungicida y principalmente como acaricida. Controla: trips, fusarium y ácaros.','2024-10-26 03:58:02','2024-10-26 03:58:02',1,'0e8ebc35-e85e-4088-8997-0f2e01af205b.png'),
(12,'Biofloripundia','Es un bionematicida y bioinsecticida que combate nematodos.','2024-10-26 04:04:58','2024-10-26 04:04:58',1,'26b3e5ab-d1da-4a0d-9925-4c7e81f18d33.png'),
(13,'Como armar un cubo de Rubik','NUEVA DESCRIPCION','2024-10-29 02:33:42','2024-10-29 02:29:49',0,'f909a46f-5953-448f-86ec-9038484c1b6f.jpg'),
(14,'Cubo Rubik 2.0','Como armar desde cero un cubo','2024-10-29 02:43:17','2024-10-29 02:39:52',0,'3b23b2c4-ba17-48cd-8e5b-9c0c41ffec89.jpg');
/*!40000 ALTER TABLE `Courses` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Faqs`
--

DROP TABLE IF EXISTS `Faqs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Faqs` (
  `faqId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `question` varchar(128) NOT NULL,
  `answer` varchar(255) DEFAULT NULL,
  `modifiedAt` timestamp NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `createdAt` timestamp NULL DEFAULT current_timestamp(),
  `status` tinyint(1) DEFAULT 1,
  PRIMARY KEY (`faqId`)
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Faqs`
--

LOCK TABLES `Faqs` WRITE;
/*!40000 ALTER TABLE `Faqs` DISABLE KEYS */;
INSERT INTO `Faqs` VALUES
(1,'¿Cuál es el enfoque del diplomado en Bioinsumos?','El diplomado en Bioinsumos se enfoca en la producción y uso de insumos biológicos para la agricultura sostenible, promoviendo prácticas agroecológicas en comunidades rurales de Guatemala.','2024-10-18 03:34:49','2024-09-21 22:55:01',0),
(2,'¿Cuánto tiempo toma completar el diplomado?','El diplomado puede completarse en uno o dos años, dependiendo del ritmo de estudio y la carga de los cursos.','2024-10-18 03:34:52','2024-09-21 22:55:38',0),
(3,'¿Cuáles son los temas principales cubiertos?','Los temas principales incluyen la producción de biofertilizantes, control biológico de plagas, compostaje y técnicas sostenibles de manejo de suelos, aplicadas a la agricultura local.','2024-10-18 03:34:56','2024-09-21 22:56:06',0),
(4,'¿Donde estamos ubicados?','En el sector las delicias de san juan comalapa.','2024-09-22 00:27:05','2024-09-22 00:16:31',0),
(5,'¿Cuál es el enfoque del diplomado en Bioinsumos?','El diplomado en Bioinsumos se enfoca en la producción y uso de insumos biológicos para la agricultura sostenible, promoviendo prácticas agroecológicas en comunidades rurales de Guatemala.','2024-10-18 03:34:59','2024-09-22 03:25:57',0),
(6,'¿Cuánto tiempo toma completar el diplomado?','El diplomado puede completarse en uno o dos años, dependiendo del ritmo de estudio y la carga de los cursos.','2024-10-18 03:35:04','2024-09-22 03:26:16',0),
(7,'¿Cuáles son los temas principales cubiertos?','Los temas principales incluyen la producción de biofertilizantes, control biológico de plagas, compostaje y técnicas sostenibles de manejo de suelos, aplicadas a la agricultura local.','2024-09-24 16:13:02','2024-09-22 03:26:37',0),
(8,'¿Cuáles son los servicios que brinda la ASPROC?','Los servicios son; intercambios de experiencias, cursos libres, diplomados en elaboración de bioinsumos y visitas en pareas productivas (invernaderos y producción a campo abierto).','2024-10-18 03:35:45','2024-10-18 03:35:45',1),
(9,'¿En donde se encuentran ubicados?','La ASPROC se ubica en el Sector las Delicias Zona 3 del Municipio de San Juan Comalapa, Chimaltenango.','2024-10-18 03:36:47','2024-10-18 03:36:09',1),
(10,'¿Qué tipo de bioinsumos desarrollan?','Los bioinsumos que se desarrollan se basan en, microorganismos de Montaña en su fase sólida y líquida, bioinsecticida, biofungicidas, biobactericidas, biofertilizantes y abonos sólidos.','2024-10-18 03:37:15','2024-10-18 03:37:15',1),
(11,'¿Cultivos que se manejan dentro de la ASPROC?','Los cultivos que se maneja bajo condiciones protegidas (invernaderos) son tomate, pepino y flores, y a campo abierto se manejan variedades de hortalizas todas con un manejo orgánico.','2024-10-18 03:37:42','2024-10-18 03:37:42',1),
(12,'pregunta modificada','Me llamo Diego','2024-10-29 02:50:09','2024-10-29 02:49:10',0),
(13,'¿COMO TE LLAMAS?','ME LLAMO PEDRO PABLO','2024-11-01 02:54:49','2024-11-01 02:53:36',0);
/*!40000 ALTER TABLE `Faqs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Handbooks`
--

DROP TABLE IF EXISTS `Handbooks`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Handbooks` (
  `handbookId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `name` varchar(128) NOT NULL,
  `description` varchar(255) DEFAULT NULL,
  `path` varchar(128) NOT NULL,
  `modifiedAt` timestamp NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `createdAt` timestamp NULL DEFAULT current_timestamp(),
  `status` tinyint(1) DEFAULT 1,
  PRIMARY KEY (`handbookId`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Handbooks`
--

LOCK TABLES `Handbooks` WRITE;
/*!40000 ALTER TABLE `Handbooks` DISABLE KEYS */;
INSERT INTO `Handbooks` VALUES
(1,'Descargar manual','Descargar Manual del Diplomado','52a6e83f-d142-410b-b8ff-7be521a3acb0.pdf','2024-09-21 05:06:03','2024-09-21 05:06:03',1);
/*!40000 ALTER TABLE `Handbooks` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `News`
--

DROP TABLE IF EXISTS `News`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `News` (
  `newId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(128) NOT NULL,
  `body` text DEFAULT NULL,
  `imgPath` varchar(128) NOT NULL,
  `url` varchar(255) DEFAULT NULL,
  `modifiedAt` timestamp NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `createdAt` timestamp NULL DEFAULT current_timestamp(),
  `status` tinyint(1) DEFAULT 1,
  PRIMARY KEY (`newId`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `News`
--

LOCK TABLES `News` WRITE;
/*!40000 ALTER TABLE `News` DISABLE KEYS */;
INSERT INTO `News` VALUES
(11,'El lombricompost','<p>El lombricompost es el producto de la digestión de residuos orgánicos por la lombriz roja californiana. Al alimentarse de dichos residuos, las lombrices los descomponen, dejándolos disponibles para la acción de microorganismos.</p><br><p>Fuente: Ministerio de Agricultura, Ganadería y Alimentación</p>','2173a1e5-aa1b-4f0e-813b-f8a537c60123.png','https://www.maga.gob.gt/el-lombricompost/','2024-10-18 03:01:32','2024-10-18 03:01:32',1),
(15,'AGRICULTURA ORGÀNICA Y ELABORACIÒN DE BIOINSUMOS','¡Llegó el diplomado tan esperado! ','f86fffce-eb85-4461-ac97-bf17a122623e.png','https://www.facebook.com/share/p/SnEpdu3c82kuHWkC/','2024-10-29 16:03:33','2024-10-29 16:03:33',1),
(16,'Invitamos a todos nuestros seguidores a sintonizar la Radio Fabulosa, el próximo lunes para escuchar alternativas efectivas.','\"OFERTA Y DEMANDA AFECTA EL PRECIO DEL TOMATE POR FALTA DE CONTROL EN LAS EXTENSIONES DE PRODUCCIÓN\"','a7bc143c-453b-4458-b2c8-1cb05cc34679.png','https://www.facebook.com/share/p/WybA8QGBhP3Psfrc/','2024-10-30 15:48:06','2024-10-30 15:48:06',1),
(17,'Aguacate Hass de Guatemala podrá exportarse en enero a Estados Unidos y estos son los pasos que faltan','El servicio fitosanitario de Guatemala ya mantiene un riguroso cumplimiento a las fincas y empacadoras de aguacate Hass con las inspecciones que se han realizado previo a los primeros despachos en enero del 2025.','7b17ccbc-c9d5-42cc-84d4-3935fd257a06.jpg','https://www.prensalibre.com/economia/aguacate-hass-de-guatemala-podra-exportarse-en-enero-a-estados-unidos-y-estos-son-los-pasos-que-faltan/','2024-11-12 17:23:43','2024-11-12 17:23:43',1);
/*!40000 ALTER TABLE `News` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Pdfs`
--

DROP TABLE IF EXISTS `Pdfs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Pdfs` (
  `pdfId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `courseId` int(10) unsigned DEFAULT NULL,
  `path` varchar(128) NOT NULL,
  `modifiedAt` timestamp NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `createdAt` timestamp NULL DEFAULT current_timestamp(),
  `status` tinyint(1) DEFAULT 1,
  PRIMARY KEY (`pdfId`),
  KEY `courseId` (`courseId`),
  CONSTRAINT `Pdfs_ibfk_1` FOREIGN KEY (`courseId`) REFERENCES `Courses` (`courseId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=14 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Pdfs`
--

LOCK TABLES `Pdfs` WRITE;
/*!40000 ALTER TABLE `Pdfs` DISABLE KEYS */;
INSERT INTO `Pdfs` VALUES
(1,1,'48cda614-735d-4d36-91ae-1ef66cfb0b3e.pdf','2024-08-26 15:46:52','2024-08-26 15:46:52',1),
(2,2,'a433b951-fbb1-44d8-83a4-d3e5ca93944f.pdf','2024-09-03 21:22:13','2024-09-03 21:22:13',1),
(3,3,'af0ea5b6-120f-4a2a-b0a3-f3b3ac0d3d55.pdf','2024-09-04 04:28:55','2024-09-04 04:28:55',1),
(4,4,'1822c729-ad35-4d4a-9f90-83a34364e434.pdf','2024-09-04 04:45:07','2024-09-04 04:45:07',1),
(5,5,'51624c5a-b462-4eed-abc5-d457aaa82fcf.pdf','2024-09-04 05:00:47','2024-09-04 05:00:47',1),
(6,6,'9e9b5f34-6604-453e-a84c-c3de1d1fe3d8.pdf','2024-09-04 05:20:12','2024-09-04 05:20:12',1),
(7,8,'007a12cc-4adc-46d8-aaac-6d388d543eaf.pdf','2024-09-04 18:35:11','2024-09-04 18:35:11',1),
(8,9,'579276ea-1e71-47ec-abdf-e24057a0cdd9.pdf','2024-09-04 20:12:27','2024-09-04 20:12:27',1),
(9,10,'9a13130e-5cb9-46da-bc5e-0a210bbe9610.pdf','2024-09-04 20:36:42','2024-09-04 20:36:42',1),
(10,11,'1fd06044-06b0-47b6-a11b-014d7cc783da.pdf','2024-10-26 03:58:31','2024-10-26 03:58:31',1),
(11,12,'590e381f-8029-48de-bead-3d65d66f228d.pdf','2024-10-26 04:05:19','2024-10-26 04:05:19',1),
(13,14,'c5ea44fc-0154-4719-a4e6-4653b2a7be0b.pdf','2024-10-29 02:41:17','2024-10-29 02:41:17',1);
/*!40000 ALTER TABLE `Pdfs` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Prices`
--

DROP TABLE IF EXISTS `Prices`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Prices` (
  `priceId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(128) NOT NULL,
  `description` varchar(128) NOT NULL,
  `price` smallint(5) unsigned DEFAULT 0,
  `imgPath` varchar(128) NOT NULL,
  `modifiedAt` timestamp NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `createdAt` timestamp NULL DEFAULT current_timestamp(),
  `status` tinyint(1) DEFAULT 1,
  PRIMARY KEY (`priceId`)
) ENGINE=InnoDB AUTO_INCREMENT=11 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Prices`
--

LOCK TABLES `Prices` WRITE;
/*!40000 ALTER TABLE `Prices` DISABLE KEYS */;
INSERT INTO `Prices` VALUES
(1,'Tomate','Caja de 50 Lbs',100,'3021cb75-b6fa-49be-a56e-03b3d04315e7.jpg','2024-11-14 16:58:56','2024-09-24 20:24:48',1),
(2,'Chile Pimiento','Caja de 28 Lbs',40,'03e03b47-04b7-48a4-bb8d-318fb7dc6e3e.jpg','2024-11-14 16:59:16','2024-09-24 20:26:16',1),
(3,'Chile Jalapeño','Costal 40 Lbs',110,'c9e831e3-51a6-4a01-8b00-292bd32ff59e.jpg','2024-11-14 16:59:35','2024-09-24 20:27:36',1),
(4,'Pepino','Costal 70 a 80 unidades',90,'56a0c09f-923d-4bdc-bd59-1926584d72d3.jpg','2024-11-14 17:00:17','2024-09-24 20:28:49',1),
(5,'Cebolla Blanca Nacional','qq',340,'a569c716-8c24-4c97-b48b-c42cf030cd40.jpg','2024-11-14 17:00:41','2024-09-24 20:29:43',1),
(6,'Cebolla Blanca Importada','qq',360,'35bfd6b6-c8d1-4d33-9cbb-46f111dd7ef0.jpg','2024-11-14 17:00:58','2024-10-18 15:49:26',1),
(7,'Papaya','Unidad',15,'19a0d9ed-5db2-42e9-b2fd-8c76a4989361.jpg','2024-10-18 15:51:09','2024-10-18 15:51:09',1),
(8,'Sandia','Metro Cubico',2500,'96b686b5-7d3d-4a7a-bee2-ad1775ca4271.jpg','2024-11-14 17:01:20','2024-10-18 15:52:40',1);
/*!40000 ALTER TABLE `Prices` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Products`
--

DROP TABLE IF EXISTS `Products`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Products` (
  `productId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(128) NOT NULL,
  `description` varchar(300) NOT NULL,
  `imgPath` varchar(128) NOT NULL,
  `modifiedAt` timestamp NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `createdAt` timestamp NULL DEFAULT current_timestamp(),
  `status` tinyint(1) DEFAULT 1,
  PRIMARY KEY (`productId`)
) ENGINE=InnoDB AUTO_INCREMENT=12 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Products`
--

LOCK TABLES `Products` WRITE;
/*!40000 ALTER TABLE `Products` DISABLE KEYS */;
INSERT INTO `Products` VALUES
(6,'M5','Controlador de insectos, su potencia es muy alto gracias a los compuestos que se generan por los elementos como el ají, capsicina, alicina (cebolla). Además, su composición permite repeler los insectos que proliferan e infectan el cultivo.','d0785c2d-a7b4-4f1c-bb99-65e6078baf9a.jpg','2024-10-18 02:40:27','2024-10-18 02:40:27',1),
(7,'Caldo Sulfocálcico','Fungicida útil para el control de nemàtodos así como en su contribución en el aporte nutrientes para fomentar el crecimiento vegetal y radicular, la floración y fructificación de los cultivos.','0124e109-1810-421b-81c1-f9744c542eb3.jpg','2024-10-18 02:41:35','2024-10-18 02:41:35',1),
(8,'APICHI','Controla insectos portadores de enfermedades, hongos, bacterias y otros patógenos que afectan la producción agrícola. Se recomienda su aplicación en la etapa de floración de hortalizas, frutales y plantas ornamentales.','1585bed5-dc73-4c99-af2f-8889d9cb4a87.jpg','2024-10-18 02:42:44','2024-10-18 02:42:44',1),
(9,'Abono tipo Bocashi','Abono orgánico sólido, producto de un proceso de fermentación que acelera la degradación de la materia orgánica y también eleva la temperatura, permitiendo la eliminación de patógenos. Este proceso es más acelerado que el compostaje y permite obtener un abono en un lapso de 15 días.','dd47fe75-6a9f-4358-932a-bb864fbe4638.jpeg','2024-10-18 02:50:09','2024-10-18 02:50:09',1),
(10,'Biofertilizantes','Nutrientes de base líquida fermentados de forma anaeróbica mediante el uso de microorganismos de montaña. ','08a14436-e7e5-4005-b8ee-b71e49dc46a4.jpg','2024-10-18 02:51:14','2024-10-18 02:51:14',1);
/*!40000 ALTER TABLE `Products` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Reviews`
--

DROP TABLE IF EXISTS `Reviews`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Reviews` (
  `reviewId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `courseId` int(10) unsigned DEFAULT NULL,
  `review` varchar(255) DEFAULT NULL,
  `score` decimal(2,1) DEFAULT NULL CHECK (`score` >= 0 and `score` <= 5),
  `name` varchar(64) DEFAULT NULL,
  `email` varchar(64) DEFAULT NULL,
  `phone` varchar(16) DEFAULT NULL,
  `response` varchar(255) DEFAULT NULL,
  `modifiedAt` timestamp NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `createdAt` timestamp NULL DEFAULT current_timestamp(),
  `status` tinyint(1) DEFAULT 1,
  PRIMARY KEY (`reviewId`),
  KEY `courseId` (`courseId`),
  CONSTRAINT `Reviews_ibfk_1` FOREIGN KEY (`courseId`) REFERENCES `Courses` (`courseId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Reviews`
--

LOCK TABLES `Reviews` WRITE;
/*!40000 ALTER TABLE `Reviews` DISABLE KEYS */;
INSERT INTO `Reviews` VALUES
(1,1,'Muy buen video',3.5,'Diego Herrera','dherreramorales99@gmail.com','45277541',NULL,'2024-08-26 18:41:23','2024-08-26 18:41:23',1),
(2,1,'HOLA HOLA HOLA',2.5,'Juan Perez','correo@correo.com','12341234',NULL,'2024-09-01 19:25:03','2024-09-01 19:25:03',1),
(3,3,'muy buen curso ',3.5,'diego  herrera','dherreramorales99@gmail.com','45277541',NULL,'2024-09-04 22:11:04','2024-09-04 22:11:04',1);
/*!40000 ALTER TABLE `Reviews` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Users`
--

DROP TABLE IF EXISTS `Users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Users` (
  `userId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `email` varchar(64) NOT NULL,
  `password` varchar(255) NOT NULL,
  `modifiedAt` timestamp NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `createdAt` timestamp NULL DEFAULT current_timestamp(),
  `status` tinyint(1) DEFAULT 1,
  PRIMARY KEY (`userId`),
  UNIQUE KEY `email` (`email`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Users`
--

LOCK TABLES `Users` WRITE;
/*!40000 ALTER TABLE `Users` DISABLE KEYS */;
INSERT INTO `Users` VALUES
(1,'asproc','7f8e7b8b8f2296f10218d603c3cd0f4607a15ac7f6ae8e1c72a961f04643edfd','2024-08-26 15:44:27','2024-08-26 15:44:27',1);
/*!40000 ALTER TABLE `Users` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Videos`
--

DROP TABLE IF EXISTS `Videos`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Videos` (
  `videoId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `courseId` int(10) unsigned DEFAULT NULL,
  `path` varchar(128) NOT NULL,
  `modifiedAt` timestamp NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `createdAt` timestamp NULL DEFAULT current_timestamp(),
  `status` tinyint(1) DEFAULT 1,
  PRIMARY KEY (`videoId`),
  KEY `courseId` (`courseId`),
  CONSTRAINT `Videos_ibfk_1` FOREIGN KEY (`courseId`) REFERENCES `Courses` (`courseId`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Videos`
--

LOCK TABLES `Videos` WRITE;
/*!40000 ALTER TABLE `Videos` DISABLE KEYS */;
INSERT INTO `Videos` VALUES
(1,1,'94d94486-6b8b-4262-947a-a190f4e073cc.mp4','2024-08-26 15:45:56','2024-08-26 15:45:56',1),
(2,2,'a2425fff-cb64-447a-97c6-e1bf096df045.mp4','2024-09-04 04:00:04','2024-09-04 04:00:04',1),
(3,3,'ef85f541-7278-47da-a2d3-70d77940a216.mp4','2024-09-04 04:36:55','2024-09-04 04:36:55',1),
(4,4,'feaea641-203d-4fdb-8991-45101a06a064.mp4','2024-09-04 04:49:03','2024-09-04 04:49:03',1),
(5,5,'cfa7a3fc-6181-470f-bbd3-6410caed246b.mp4','2024-09-04 05:12:30','2024-09-04 05:12:30',1),
(6,6,'80e09032-90aa-4159-af85-696818da9b37.mp4','2024-09-04 05:24:56','2024-09-04 05:24:56',1),
(7,8,'4d3219e1-1d05-4f07-a89c-33e354f01fec.mp4','2024-09-04 18:47:49','2024-09-04 18:47:49',1),
(8,9,'43e0f049-cadb-45d9-93a2-0b6f015b95e6.mp4','2024-09-04 20:16:18','2024-09-04 20:16:18',1),
(9,10,'c66275e9-794c-494c-b75a-78f89a09ee29.mp4','2024-09-04 20:39:56','2024-09-04 20:39:56',1),
(10,11,'1db5d746-dfa2-46c3-ba56-aef92a865bd5.mp4','2024-10-26 04:02:39','2024-10-26 04:02:39',1),
(11,12,'1e063442-ebb5-450c-9549-66faf5f55315.mp4','2024-10-26 04:11:40','2024-10-26 04:11:40',1),
(12,13,'fde767f9-a703-42c6-9d9e-8a1a31f7fc5d.mp4','2024-10-29 02:31:03','2024-10-29 02:31:03',1);
/*!40000 ALTER TABLE `Videos` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `Wheater_Alerts`
--

DROP TABLE IF EXISTS `Wheater_Alerts`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `Wheater_Alerts` (
  `weatherAlertId` int(10) unsigned NOT NULL AUTO_INCREMENT,
  `title` varchar(128) NOT NULL,
  `body` text DEFAULT NULL,
  `imgPath` varchar(128) NOT NULL,
  `url` varchar(128) DEFAULT NULL,
  `modifiedAt` timestamp NULL DEFAULT current_timestamp() ON UPDATE current_timestamp(),
  `createdAt` timestamp NULL DEFAULT current_timestamp(),
  `status` tinyint(1) DEFAULT 1,
  PRIMARY KEY (`weatherAlertId`)
) ENGINE=InnoDB AUTO_INCREMENT=18 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_uca1400_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `Wheater_Alerts`
--

LOCK TABLES `Wheater_Alerts` WRITE;
/*!40000 ALTER TABLE `Wheater_Alerts` DISABLE KEYS */;
INSERT INTO `Wheater_Alerts` VALUES
(14,'Clima en Guatemala: Vaguada monzónica y onda del este traerán lluvias esta semana','El Insivumeh informa que se esperan lluvias esta semana en Guatemala y monitorea una zona de posible formación ciclónica en el mar Caribe.','07fefbca-2d4b-4e8d-b29a-a45294bca7ab.jpeg','https://acortar.link/lQMaRi','2024-11-12 21:27:01','2024-11-12 21:27:01',1),
(16,'Sinopsis de Boletín Especial #72-2024','14 DE NOVIEMBRE DE 2024, 13:00 HORA LOCAL','d0cd0a6c-c562-41ed-af00-396d445e1662.jpeg','https://www.facebook.com/share/p/1AhaVCVMJz/','2024-11-14 20:27:07','2024-11-14 20:27:07',1),
(17,'Tormenta tropical Sara promueve lluvias en regiones del país.','Avisos Informativos','5289e780-35bd-4579-b23e-d27ef105bfce.jpg','https://www.facebook.com/share/p/1ASYMrrQTV/','2024-11-15 19:55:46','2024-11-15 19:55:46',1);
/*!40000 ALTER TABLE `Wheater_Alerts` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2024-11-24 18:39:53
