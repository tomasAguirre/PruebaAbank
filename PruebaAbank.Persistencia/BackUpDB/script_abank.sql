--
-- PostgreSQL database dump
--

\restrict 2ECgHoqUeaRg7pZGLxsX12Un0iR6RmaXlRPJUTXlsmcSYVpDBSjhK0sNwxk0seE

-- Dumped from database version 18.6
-- Dumped by pg_dump version 18.6

-- Started on 2026-10-03 09:19:16

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 221 (class 1255 OID 24601)
-- Name: actualizar_fecha_modificacion(); Type: FUNCTION; Schema: public; Owner: postgres
--

CREATE FUNCTION public.actualizar_fecha_modificacion() RETURNS trigger
    LANGUAGE plpgsql
    AS $$
BEGIN
    NEW.fecha_modificacion = CURRENT_TIMESTAMP;
    RETURN NEW;
END;
$$;


ALTER FUNCTION public.actualizar_fecha_modificacion() OWNER TO postgres;

--
-- TOC entry 219 (class 1259 OID 24577)
-- Name: usuarios_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.usuarios_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.usuarios_id_seq OWNER TO postgres;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 220 (class 1259 OID 24578)
-- Name: usuarios; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.usuarios (
    id integer DEFAULT nextval('public.usuarios_id_seq'::regclass) NOT NULL,
    nombres character varying(100) NOT NULL,
    apellidos character varying(100) NOT NULL,
    fecha_nacimiento date NOT NULL,
    direccion character varying(255) NOT NULL,
    password character varying(120) NOT NULL,
    telefono character varying(20) NOT NULL,
    email character varying(150) NOT NULL,
    estado character varying(1) DEFAULT 'A'::character varying NOT NULL,
    fecha_creacion timestamp without time zone DEFAULT CURRENT_TIMESTAMP NOT NULL,
    fecha_modificacion timestamp without time zone,
    CONSTRAINT ck_usuarios_estado CHECK (((estado)::text = ANY ((ARRAY['A'::character varying, 'I'::character varying])::text[])))
);


ALTER TABLE public.usuarios OWNER TO postgres;

--
-- TOC entry 5014 (class 0 OID 24578)
-- Dependencies: 220
-- Data for Name: usuarios; Type: TABLE DATA; Schema: public; Owner: postgres
--

COPY public.usuarios (id, nombres, apellidos, fecha_nacimiento, direccion, password, telefono, email, estado, fecha_creacion, fecha_modificacion) FROM stdin;
5	Jonathan	Aguirre	1992-10-02	San Salvador, El Salvador	MiPasswordSeguro123	7890-1234	jairo.aguirre@abank.com.sv	A	2026-10-02 23:07:54.187431	\N
4	Jonathan	Aguirre	1992-10-02	San Salvador, El Salvador	MiPasswordSeguro123	7890-1234	ag.aguirre@abank.com.sv	I	2026-10-02 22:23:06.010212	2026-10-03 09:08:14.084054
\.


--
-- TOC entry 5020 (class 0 OID 0)
-- Dependencies: 219
-- Name: usuarios_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.usuarios_id_seq', 5, true);


--
-- TOC entry 4862 (class 2606 OID 24598)
-- Name: usuarios pk_usuarios; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuarios
    ADD CONSTRAINT pk_usuarios PRIMARY KEY (id);


--
-- TOC entry 4864 (class 2606 OID 24600)
-- Name: usuarios usuarios_email_key; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.usuarios
    ADD CONSTRAINT usuarios_email_key UNIQUE (email);


--
-- TOC entry 4865 (class 2620 OID 24602)
-- Name: usuarios trg_actualizar_usuarios; Type: TRIGGER; Schema: public; Owner: postgres
--

CREATE TRIGGER trg_actualizar_usuarios BEFORE UPDATE ON public.usuarios FOR EACH ROW EXECUTE FUNCTION public.actualizar_fecha_modificacion();


-- Completed on 2026-10-03 09:19:16

--
-- PostgreSQL database dump complete
--

\unrestrict 2ECgHoqUeaRg7pZGLxsX12Un0iR6RmaXlRPJUTXlsmcSYVpDBSjhK0sNwxk0seE

