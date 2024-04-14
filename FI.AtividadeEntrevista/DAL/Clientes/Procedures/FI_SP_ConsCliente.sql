DELIMITER //

CREATE PROCEDURE sp_conscliente(IN p_ID BIGINT)
BEGIN
    IF p_ID IS NULL OR p_ID = 0 THEN
        SELECT Nome, Sobrenome, Nacionalidade, CEP, Estado, Cidade, Logradouro, Email, Telefone, CPF, Id FROM clientes;
    ELSE
        SELECT Nome, Sobrenome, Nacionalidade, CEP, Estado, Cidade, Logradouro, Email, Telefone, CPF, Id FROM clientes WHERE Id = p_ID;
    END IF;
END //

DELIMITER ;
