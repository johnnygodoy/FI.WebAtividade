-- fiatividade.clientes definition

CREATE TABLE `clientes` (
  `Id` int NOT NULL AUTO_INCREMENT,
  `Nome` varchar(50) NOT NULL,
  `Sobrenome` varchar(255) NOT NULL,
  `Nacionalidade` varchar(50) DEFAULT NULL,
  `CEP` varchar(9) NOT NULL,
  `Estado` varchar(2) NOT NULL,
  `Cidade` varchar(50) NOT NULL,
  `Logradouro` varchar(500) NOT NULL,
  `Email` varchar(255) DEFAULT NULL,
  `Telefone` varchar(15) DEFAULT NULL,
  `CPF` char(14) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `CPF` (`CPF`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;