CREATE OR REPLACE PROCEDURE fi_sp_conscliente(_id BIGINT)
LANGUAGE plpgsql
AS $$
BEGIN
    DELETE FROM clientes WHERE ID = _id;
END;
$$;
