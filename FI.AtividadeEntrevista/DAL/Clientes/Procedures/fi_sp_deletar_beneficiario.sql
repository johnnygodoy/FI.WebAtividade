CREATE OR REPLACE PROCEDURE public.fi_sp_alterar_beneficiario(
    _id BIGINT,
    _cpf TEXT,
    _nome TEXT
)
LANGUAGE plpgsql
AS $procedure$
BEGIN
    UPDATE beneficiarios
    SET cpf = _cpf, nome = _nome
    WHERE id = _id;
END;
$procedure$;