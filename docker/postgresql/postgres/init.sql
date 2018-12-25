create database liblazor
with owner postgres;
DO
$do$
BEGIN
   IF NOT EXISTS
      (
          SELECT
          FROM   pg_catalog.pg_user
          WHERE  usename = 'appuser'
      )
      THEN
      CREATE USER appuser WITH PASSWORD 'libraryUser1997';
      GRANT ALL ON DATABASE liblazor TO appuser;
      GRANT ALL ON ALL TABLES IN SCHEMA public TO appuser;
      GRANT ALL PRIVILEGES ON ALL TABLES IN SCHEMA public TO appuser;
      GRANT ALL PRIVILEGES ON ALL SEQUENCES IN SCHEMA public TO appuser;
   END IF;
END
$do$;