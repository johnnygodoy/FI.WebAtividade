CREATE DEFINER=`root`@`localhost` PROCEDURE `fiatividade`.`fi_sp_incliente_v2`(
    IN p_NOME VARCHAR(255),
    IN p_SOBRENOME VARCHAR(255),
    IN p_NACIONALIDADE VARCHAR(255),
    IN p_CEP VARCHAR(255),
    IN p_ESTADO VARCHAR(255),
    IN p_CIDADE VARCHAR(255),
    IN p_LOGRADOURO VARCHAR(255),
    IN p_EMAIL VARCHAR(255),
    IN p_TELEFONE VARCHAR(255),
    IN p_CPF CHAR(14)
)
BEGIN
    DECLARE v_ID BIGINT;
    
    INSERT INTO clientes (Nome, Sobrenome, Nacionalidade, CEP, Estado, Cidade, Logradouro, Email, Telefone, CPF) 
    VALUES (p_NOME, p_SOBRENOME, p_NACIONALIDADE, p_CEP, p_ESTADO, p_CIDADE, p_LOGRADOURO, p_EMAIL, p_TELEFONE, p_CPF);

    SET v_ID = LAST_INSERT_ID();

    SELECT v_ID;
END

