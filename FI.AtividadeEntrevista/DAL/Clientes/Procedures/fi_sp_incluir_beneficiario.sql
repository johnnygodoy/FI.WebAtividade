CREATE OR REPLACE PROCEDURE public.fi_sp_incluir_beneficiario(
    _cpf TEXT,
    _nome TEXT,
    _id_cliente BIGINT
)
LANGUAGE plpgsql
AS $procedure$
BEGIN
    INSERT INTO beneficiarios (cpf, nome, idcliente)
    VALUES (_cpf, _nome, _id_cliente);
END;
$procedure$;