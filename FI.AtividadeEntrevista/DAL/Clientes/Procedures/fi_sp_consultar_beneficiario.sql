CREATE DEFINER=`root`@`localhost` PROCEDURE `fiatividade`.`fi_sp_consultar_beneficiario`(
    IN p_cpf CHAR(14),
    OUT p_existe_beneficiario BOOLEAN
)
BEGIN
    DECLARE v_id_cliente INT;

    -- Verificar se existe um cliente com o CPF fornecido
    SELECT Id INTO v_id_cliente FROM clientes WHERE CPF = p_cpf;

    -- Verificar se existe um beneficiário associado ao cliente
    IF v_id_cliente IS NOT NULL THEN
        SET p_existe_beneficiario = TRUE;
    ELSE
        SET p_existe_beneficiario = FALSE;
    END IF;
    
END;