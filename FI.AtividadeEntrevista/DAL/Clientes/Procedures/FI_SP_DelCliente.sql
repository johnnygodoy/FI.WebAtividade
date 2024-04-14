CREATE DEFINER=`root`@`localhost` PROCEDURE `fiatividade`.`fi_sp_DelCliente`(IN p_id BIGINT)
BEGIN
    DELETE FROM clientes WHERE ID = p_id;
END;