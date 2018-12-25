FROM postgres:latest


#ENV POSTGRES_PASSWORD tracedouser
#ENV POSTGRES_DB tracedouser_password

# Backup file for restoring
#COPY db_backup/localbackup /db_backup01/localbackup
#COPY db_backup/script.sql /db_backup01/script.sql

# Start sql script
#COPY init.sql /docker-entrypoint-initdb.d/ #will start on every docker-compose up

RUN mkdir /home/postgres && \
    touch /home/postgres/.psql_history \
    chmod 777 .psql_history

