-- fiatividade.beneficiarios definition

CREATE TABLE `beneficiarios` (
  `Id` bigint NOT NULL AUTO_INCREMENT,
  `CPF` varchar(11) NOT NULL,
  `Nome` varchar(100) NOT NULL,
  `IdCliente` int DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IdCliente` (`IdCliente`),
  CONSTRAINT `beneficiarios_ibfk_1` FOREIGN KEY (`IdCliente`) REFERENCES `clientes` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;