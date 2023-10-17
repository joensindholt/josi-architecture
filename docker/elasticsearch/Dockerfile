FROM elasticsearch:8.10.2
COPY . .
USER root
RUN chmod +x entrypoint.sh
USER elasticsearch
ENTRYPOINT ./entrypoint.sh
