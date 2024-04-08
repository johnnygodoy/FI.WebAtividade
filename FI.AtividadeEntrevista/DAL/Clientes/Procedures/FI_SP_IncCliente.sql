﻿CREATE OR REPLACE PROCEDURE fi_sp_incliente_v2(
    _NOME          VARCHAR,
    _SOBRENOME     VARCHAR,
    _NACIONALIDADE VARCHAR,
    _CEP           VARCHAR,
    _ESTADO        VARCHAR,
    _CIDADE        VARCHAR,
    _LOGRADOURO    VARCHAR,
    _EMAIL         VARCHAR,
    _TELEFONE      VARCHAR,
    _CPF           CHAR(14)
)
LANGUAGE plpgsql
AS $$
BEGIN
    INSERT INTO clientes (NOME, SOBRENOME, NACIONALIDADE, CEP, ESTADO, CIDADE, LOGRADOURO, EMAIL, TELEFONE, CPF) 
    VALUES (_NOME, _SOBRENOME, _NACIONALIDADE, _CEP, _ESTADO, _CIDADE, _LOGRADOURO, _EMAIL, _TELEFONE, _CPF)
    RETURNING ID;
END;
$$;