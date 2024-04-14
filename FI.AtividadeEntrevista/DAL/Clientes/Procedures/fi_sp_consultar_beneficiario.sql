DELIMITER //

CREATE PROCEDURE fi_sp_consultar_beneficiario(
    IN p_id BIGINT,
    OUT p_nome VARCHAR(255),
    OUT p_sobrenome VARCHAR(255),
    OUT p_nacionalidade VARCHAR(255),
    OUT p_cep VARCHAR(255),
    OUT p_estado VARCHAR(255),
    OUT p_cidade VARCHAR(255),
    OUT p_logradouro VARCHAR(255),
    OUT p_email VARCHAR(255),
    OUT p_telefone VARCHAR(255),
    OUT p_cpf CHAR(14)
)
BEGIN
    DECLARE done INT DEFAULT 0;
    DECLARE v_nome VARCHAR(255);
    DECLARE v_sobrenome VARCHAR(255);
    DECLARE v_nacionalidade VARCHAR(255);
    DECLARE v_cep VARCHAR(255);
    DECLARE v_estado VARCHAR(255);
    DECLARE v_cidade VARCHAR(255);
    DECLARE v_logradouro VARCHAR(255);
    DECLARE v_email VARCHAR(255);
    DECLARE v_telefone VARCHAR(255);
    DECLARE v_cpf CHAR(14);
    
    DECLARE cur CURSOR FOR
        SELECT nome, sobrenome, nacionalidade, cep, estado, cidade, logradouro, email, telefone, cpf
        FROM beneficiario WHERE id = p_id;
    DECLARE CONTINUE HANDLER FOR NOT FOUND SET done = 1;

    OPEN cur;
    FETCH cur INTO v_nome, v_sobrenome, v_nacionalidade, v_cep, v_estado, v_cidade, v_logradouro, v_email, v_telefone, v_cpf;

    IF done THEN
        CLOSE cur;
        SET p_nome = NULL;
        SET p_sobrenome = NULL;
        SET p_nacionalidade = NULL;
        SET p_cep = NULL;
        SET p_estado = NULL;
        SET p_cidade = NULL;
        SET p_logradouro = NULL;
        SET p_email = NULL;
        SET p_telefone = NULL;
        SET p_cpf = NULL;
    ELSE
        CLOSE cur;
        SET p_nome = v_nome;
        SET p_sobrenome = v_sobrenome;
        SET p_nacionalidade = v_nacionalidade;
        SET p_cep = v_cep;
        SET p_estado = v_estado;
        SET p_cidade = v_cidade;
        SET p_logradouro = v_logradouro;
        SET p_email = v_email;
        SET p_telefone = v_telefone;
        SET p_cpf = v_cpf;
    END IF;
END //

DELIMITER ;
