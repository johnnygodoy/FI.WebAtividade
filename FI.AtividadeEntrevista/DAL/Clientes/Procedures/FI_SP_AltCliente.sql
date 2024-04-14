CREATE DEFINER=`root`@`localhost` PROCEDURE `fiatividade`.`FI_SP_AltCliente`(
    IN p_NOME VARCHAR(50),
    IN p_SOBRENOME VARCHAR(255),
    IN p_NACIONALIDADE VARCHAR(50),
    IN p_CEP VARCHAR(9),
    IN p_ESTADO VARCHAR(2),
    IN p_CIDADE VARCHAR(50),
    IN p_LOGRADOURO VARCHAR(500),
    IN p_EMAIL VARCHAR(2079),
    IN p_TELEFONE VARCHAR(15),
    IN p_Id BIGINT
)
BEGIN
    UPDATE clientes 
    SET 
        Nome = p_NOME,
        Sobrenome = p_SOBRENOME,
        Nacionalidade = p_NACIONALIDADE,
        CEP = p_CEP,
        Estado = p_ESTADO,
        Cidade = p_CIDADE,
        Logradouro = p_LOGRADOURO,
        Email = p_EMAIL,
        Telefone = p_TELEFONE
    WHERE Id = p_Id;
END;