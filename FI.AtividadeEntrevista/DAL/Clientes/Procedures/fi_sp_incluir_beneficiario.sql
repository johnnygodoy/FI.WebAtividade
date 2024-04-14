DELIMITER //

CREATE PROCEDURE fi_sp_incluir_beneficiario(
    IN p_cpf TEXT,
    IN p_nome TEXT,
    IN p_id_cliente BIGINT
)
BEGIN
    INSERT INTO beneficiarios (cpf, nome, idcliente)
    VALUES (p_cpf, p_nome, p_id_cliente);
END //

DELIMITER ;
