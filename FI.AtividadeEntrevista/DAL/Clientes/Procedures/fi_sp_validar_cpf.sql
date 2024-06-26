CREATE DEFINER=`root`@`localhost` PROCEDURE `fiatividade`.`fi_sp_validar_cpf`(
    IN cpf VARCHAR(14),
    OUT valido BOOLEAN
)
BEGIN
    DECLARE v_soma INT;
    DECLARE v_resto INT;
    DECLARE v_digito_verificador1 INT;
    DECLARE v_digito_verificador2 INT;
    DECLARE cpf_valido BOOLEAN DEFAULT FALSE;

    -- Remover caracteres n�o num�ricos do CPF
    SET cpf = REPLACE(cpf, '.', '');
    SET cpf = REPLACE(cpf, '-', '');

    -- Verificar se o CPF possui 11 d�gitos
    IF LENGTH(cpf) = 11 THEN
        SET v_soma = 0;

        -- Calcular o primeiro d�gito verificador
        SET v_soma = (
            SELECT 
                (SUBSTRING(cpf, 1, 1) * 10) + 
                (SUBSTRING(cpf, 2, 1) * 9) + 
                (SUBSTRING(cpf, 3, 1) * 8) + 
                (SUBSTRING(cpf, 4, 1) * 7) + 
                (SUBSTRING(cpf, 5, 1) * 6) + 
                (SUBSTRING(cpf, 6, 1) * 5) + 
                (SUBSTRING(cpf, 7, 1) * 4) + 
                (SUBSTRING(cpf, 8, 1) * 3) + 
                (SUBSTRING(cpf, 9, 1) * 2)
        ) MOD 11;

        IF v_soma < 2 THEN
            SET v_digito_verificador1 = 0;
        ELSE
            SET v_digito_verificador1 = 11 - v_soma;
        END IF;

        -- Calcular o segundo d�gito verificador
        SET v_soma = (
            SELECT 
                (SUBSTRING(cpf, 1, 1) * 11) + 
                (SUBSTRING(cpf, 2, 1) * 10) + 
                (SUBSTRING(cpf, 3, 1) * 9) + 
                (SUBSTRING(cpf, 4, 1) * 8) + 
                (SUBSTRING(cpf, 5, 1) * 7) + 
                (SUBSTRING(cpf, 6, 1) * 6) + 
                (SUBSTRING(cpf, 7, 1) * 5) + 
                (SUBSTRING(cpf, 8, 1) * 4) + 
                (SUBSTRING(cpf, 9, 1) * 3) + 
                (v_digito_verificador1 * 2)
        ) MOD 11;

        IF v_soma < 2 THEN
            SET v_digito_verificador2 = 0;
        ELSE
            SET v_digito_verificador2 = 11 - v_soma;
        END IF;

        -- Verificar se os d�gitos verificadores calculados correspondem aos do CPF
        IF v_digito_verificador1 = SUBSTRING(cpf, 10, 1) AND v_digito_verificador2 = SUBSTRING(cpf, 11, 1) THEN
            SET cpf_valido = TRUE;
        END IF;
    END IF;

    -- Retornar se o CPF � v�lido ou n�o
    SET valido = cpf_valido;
END;