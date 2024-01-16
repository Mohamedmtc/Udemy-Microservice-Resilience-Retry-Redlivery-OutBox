FROM rabbitmq:management

# Install curl
RUN apt-get update && apt-get install -y curl

# Download and install the RabbitMQ Delayed Message Exchange plugin
# Replace the URL with the correct one for your RabbitMQ version
RUN curl -L https://github.com/rabbitmq/rabbitmq-delayed-message-exchange/releases/download/v3.12.0/rabbitmq_delayed_message_exchange-3.12.0.ez -o /plugins/rabbitmq_delayed_message_exchange.ez \
    && rabbitmq-plugins enable rabbitmq_delayed_message_exchange
    

#docker build -t delayed-rabbitmq-management-final-demo .