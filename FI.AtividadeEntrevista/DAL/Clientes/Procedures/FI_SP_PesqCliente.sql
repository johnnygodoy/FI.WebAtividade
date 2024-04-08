CREATE OR REPLACE FUNCTION fi_sp_pesqcliente(_iniciarem INT, _quantidade INT, _campoordenacao TEXT, _crescente BIT)
RETURNS TABLE(id BIGINT, nome TEXT, sobrenome TEXT, nacionalidade TEXT, cep TEXT, estado TEXT, cidade TEXT, logradouro TEXT, email TEXT, telefone TEXT, cpf TEXT) AS $$
DECLARE
    _order TEXT;
BEGIN
    IF _campoordenacao = 'EMAIL' THEN
        _order := 'EMAIL';
    ELSIF _campoordenacao = 'CPF' THEN -- Adaptado para incluir CPF
        _order := 'CPF';
    ELSE
        _order := 'NOME';
    END IF;
    
    IF _crescente = 0 THEN
        _order := _order || ' DESC';
    ELSE
        _order := _order || ' ASC';
    END IF;

    RETURN QUERY EXECUTE 
    format('SELECT id, nome, sobrenome, nacionalidade, cep, estado, cidade, logradouro, email, telefone, cpf FROM (SELECT *, ROW_NUMBER() OVER (ORDER BY %I) AS row FROM clientes) AS clienteswithrownumbers WHERE row > $1 AND row <= ($1 + $2) ORDER BY %I', _order, _order)
    USING _iniciarem, _quantidade;
    
    -- Note que COUNT não é retornado como parte da tabela, isso poderia ser executado separadamente ou ajustado conforme a necessidade
END;
$$ LANGUAGE plpgsql;
