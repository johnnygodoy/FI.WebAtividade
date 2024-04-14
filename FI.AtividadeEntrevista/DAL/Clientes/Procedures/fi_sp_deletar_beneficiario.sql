DELIMITER //

CREATE PROCEDURE fi_sp_alterar_beneficiario(
    IN p_id BIGINT,
    IN p_cpf TEXT,
    IN p_nome TEXT
)
BEGIN
    UPDATE beneficiarios
    SET cpf = p_cpf, nome = p_nome
    WHERE id = p_id;
END //

DELIMITER ;
