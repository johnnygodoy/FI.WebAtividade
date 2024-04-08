CREATE OR REPLACE FUNCTION fi_sp_altcliente(
    _NOME          VARCHAR,
    _SOBRENOME     VARCHAR,
    _NACIONALIDADE VARCHAR,
    _CEP           VARCHAR,
    _ESTADO        VARCHAR,
    _CIDADE        VARCHAR,
    _LOGRADOURO    VARCHAR,
    _EMAIL         VARCHAR,
    _TELEFONE      VARCHAR,
    _CPF           CHAR(14), -- Adicionando o CPF na lista de parâmetros
    _ID            BIGINT
) RETURNS VOID AS $$
BEGIN
    UPDATE clientes 
    SET 
        NOME = _NOME, 
        SOBRENOME = _SOBRENOME, 
        NACIONALIDADE = _NACIONALIDADE, 
        CEP = _CEP, 
        ESTADO = _ESTADO, 
        CIDADE = _CIDADE, 
        LOGRADOURO = _LOGRADOURO, 
        EMAIL = _EMAIL, 
        TELEFONE = _TELEFONE,
        CPF = _CPF  -- Adicionando o campo CPF na operação de UPDATE
    WHERE ID = _ID;
END;
$$ LANGUAGE plpgsql;
