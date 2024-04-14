CREATE PROCEDURE fi_sp_validar_cpf(
    IN p_cpf VARCHAR(14),
    OUT p_valido BOOLEAN
)
BEGIN
    DECLARE v_soma INT;
    DECLARE v_resto INT;
    DECLARE v_digito_verificador1 INT;
    DECLARE v_digito_verificador2 INT;
    DECLARE v_cpf_valido BOOLEAN DEFAULT FALSE;

    -- Remover caracteres não numéricos do CPF
    SET p_cpf = REPLACE(p_cpf, '.', '');
    SET p_cpf = REPLACE(p_cpf, '-', '');

    -- Verificar se o CPF possui 11 dígitos
    IF LENGTH(p_cpf) = 11 THEN
        SET v_soma = 0;

        -- Calcular o primeiro dígito verificador
        SET v_soma = (
            SELECT 
                (SUBSTRING(p_cpf, 1, 1) * 10) + 
                (SUBSTRING(p_cpf, 2, 1) * 9) + 
                (SUBSTRING(p_cpf, 3, 1) * 8) + 
                (SUBSTRING(p_cpf, 4, 1) * 7) + 
                (SUBSTRING(p_cpf, 5, 1) * 6) + 
                (SUBSTRING(p_cpf, 6, 1) * 5) + 
                (SUBSTRING(p_cpf, 7, 1) * 4) + 
                (SUBSTRING(p_cpf, 8, 1) * 3) + 
                (SUBSTRING(p_cpf, 9, 1) * 2)
        ) MOD 11;

        IF v_soma < 2 THEN
            SET v_digito_verificador1 = 0;
        ELSE
            SET v_digito_verificador1 = 11 - v_soma;
        END IF;

        -- Calcular o segundo dígito verificador
        SET v_soma = (
            SELECT 
                (SUBSTRING(p_cpf, 1, 1) * 11) + 
                (SUBSTRING(p_cpf, 2, 1) * 10) + 
                (SUBSTRING(p_cpf, 3, 1) * 9) + 
                (SUBSTRING(p_cpf, 4, 1) * 8) + 
                (SUBSTRING(p_cpf, 5, 1) * 7) + 
                (SUBSTRING(p_cpf, 6, 1) * 6) + 
                (SUBSTRING(p_cpf, 7, 1) * 5) + 
                (SUBSTRING(p_cpf, 8, 1) * 4) + 
                (SUBSTRING(p_cpf, 9, 1) * 3) + 
                (v_digito_verificador1 * 2)
        ) MOD 11;

        IF v_soma < 2 THEN
            SET v_digito_verificador2 = 0;
        ELSE
            SET v_digito_verificador2 = 11 - v_soma;
        END IF;

        -- Verificar se os dígitos verificadores calculados correspondem aos do CPF
        IF v_digito_verificador1 = SUBSTRING(p_cpf, 10, 1) AND v_digito_verificador2 = SUBSTRING(p_cpf, 11, 1) THEN
            SET v_cpf_valido = TRUE;
        END IF;
    END IF;

    -- Retornar se o CPF é válido ou não
    SET p_valido = v_cpf_valido;
END