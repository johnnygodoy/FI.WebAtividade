DELIMITER //

CREATE FUNCTION fi_sp_pesqcliente(
    p_iniciarem INT,
    p_quantidade INT,
    p_campoordenacao TEXT,
    p_crescente BOOLEAN
)
RETURNS TABLE(
    id BIGINT,
    nome TEXT,
    sobrenome TEXT,
    nacionalidade TEXT,
    cep TEXT,
    estado TEXT,
    cidade TEXT,
    logradouro TEXT,
    email TEXT,
    telefone TEXT,
    cpf TEXT
)
BEGIN
    DECLARE v_order TEXT;

    IF p_campoordenacao = 'EMAIL' THEN
        SET v_order := 'EMAIL';
    ELSEIF p_campoordenacao = 'CPF' THEN
        SET v_order := 'CPF';
    ELSE
        SET v_order := 'NOME';
    END IF;

    IF NOT p_crescente THEN
        SET v_order := CONCAT(v_order, ' DESC');
    ELSE
        SET v_order := CONCAT(v_order, ' ASC');
    END IF;

    RETURN (
        SELECT id, nome, sobrenome, nacionalidade, cep, estado, cidade, logradouro, email, telefone, cpf 
        FROM (
            SELECT *, ROW_NUMBER() OVER (ORDER BY v_order) AS row 
            FROM clientes
        ) AS clienteswithrownumbers 
        WHERE row > p_iniciarem AND row <= (p_iniciarem + p_quantidade) 
        ORDER BY v_order
    );
END //

DELIMITER ;
