CREATE OR REPLACE PROCEDURE public.fi_sp_consultar_beneficiario(
    _id BIGINT
)
LANGUAGE plpgsql
AS $procedure$
DECLARE
    beneficiario RECORD;
BEGIN
    SELECT * INTO beneficiario FROM beneficiarios WHERE id = _id;
    RETURN beneficiario;
END;
$procedure$;