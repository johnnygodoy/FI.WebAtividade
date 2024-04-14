DELIMITER //

CREATE PROCEDURE sp_altcliente(
    IN p_NOME VARCHAR(255),
    IN p_SOBRENOME VARCHAR(255),
    IN p_NACIONALIDADE VARCHAR(255),
    IN p_CEP VARCHAR(10),
    IN p_ESTADO VARCHAR(255),
    IN p_CIDADE VARCHAR(255),
    IN p_LOGRADOURO VARCHAR(255),
    IN p_EMAIL VARCHAR(255),
    IN p_TELEFONE VARCHAR(20),
    IN p_CPF CHAR(11), 
    IN p_ID BIGINT
)
BEGIN
    UPDATE Clientes 
    SET 
        Nome = p_NOME, 
        Sobrenome = p_SOBRENOME, 
        Nacionalidade = p_NACIONALIDADE, 
        CEP = p_CEP, 
        Estado = p_ESTADO, 
        Cidade = p_CIDADE, 
        Logradouro = p_LOGRADOURO, 
        Email = p_EMAIL, 
        Telefone = p_TELEFONE,
        CPF = p_CPF  
    WHERE Id = p_ID;
END //

DELIMITER //

CREATE PROCEDURE sp_altbeneficiario(
    IN p_CPF CHAR(11),
    IN p_NOME VARCHAR(255),
    IN p_IdCliente BIGINT,
    IN p_ID BIGINT
)
BEGIN
    UPDATE Beneficiario 
    SET 
        CPF = p_CPF,
        Nome = p_NOME,
        IdCliente = p_IdCliente
    WHERE Id = p_ID;
END //

DELIMITER ;
