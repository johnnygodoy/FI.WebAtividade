CREATE PROCEDURE FI_SP_PesqCliente (
    p_iniciarEm INT,
    p_quantidade INT,
    p_campoOrdenacao VARCHAR(200),
    p_crescente BOOLEAN
)
BEGIN
    DECLARE ORDER_BY VARCHAR(50);
    
    IF (p_campoOrdenacao = 'EMAIL') THEN
        SET ORDER_BY = 'EMAIL';
    ELSE
        SET ORDER_BY = 'NOME';
    END IF;
    
    IF (p_crescente = 0) THEN
        SET ORDER_BY = CONCAT(ORDER_BY, ' DESC');
    ELSE
        SET ORDER_BY = CONCAT(ORDER_BY, ' ASC');
    END IF;

    SET @SCRIPT = CONCAT('SELECT ID, NOME, SOBRENOME, NACIONALIDADE, CEP, ESTADO, CIDADE, LOGRADOURO, EMAIL, TELEFONE 
                        FROM (SELECT @row_number:=@row_number+1 AS RowNumber, ID, NOME, SOBRENOME, NACIONALIDADE, CEP, ESTADO, CIDADE, LOGRADOURO, EMAIL, TELEFONE 
                              FROM CLIENTES, (SELECT @row_number:=0) AS rn 
                              ORDER BY ', ORDER_BY, ') AS ClientesWithRowNumbers 
                        WHERE RowNumber > ? AND RowNumber <= (?+?)');

    SET @iniciarEm = p_iniciarEm;
    SET @quantidade = p_quantidade;
    
    PREPARE stmt FROM @SCRIPT;
    EXECUTE stmt USING @iniciarEm, @iniciarEm, @quantidade;
    DEALLOCATE PREPARE stmt;

    SELECT COUNT(*) FROM CLIENTES;
END;