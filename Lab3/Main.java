// Це паттерн Singleton
class Server {
    private static Server instance;

    private Server() {}

    public static Server getInstance() {
        if (instance == null) {
            instance = new Server();
        }
        return instance;
    }

    public void handleRequest(Request request) {
        HandlerChain chain = new AuthenticationHandler();
        chain.addHandler(new LoggingHandler());
        chain.addHandler(new RequestHandler());
        chain.handle(request);
    }
}

// Це паттерн Chain of Responsibility
abstract class HandlerChain {
    private HandlerChain next;

    public void addHandler(HandlerChain next) {
        if (this.next == null) {
            this.next = next;
        } else {
            this.next.addHandler(next);
        }
    }

    public void handle(Request request) {
        if (next != null) {
            next.handle(request);
        }
    }
}

class AuthenticationHandler extends HandlerChain {
    @Override
    public void handle(Request request) {
        System.out.println("Authentication handled for: " + request.getData());
        super.handle(request);
    }
}

class LoggingHandler extends HandlerChain {
    @Override
    public void handle(Request request) {
        System.out.println("Request logged for: " + request.getData());
        super.handle(request);
    }
}

class RequestHandler extends HandlerChain {
    @Override
    public void handle(Request request) {
        System.out.println("Request processed for: " + request.getData());
        super.handle(request);
    }
}

// Це паттерн Decorator
abstract class RequestDecorator extends HandlerChain {
    protected HandlerChain decoratedHandler;

    public RequestDecorator(HandlerChain decoratedHandler) {
        this.decoratedHandler = decoratedHandler;
    }

    @Override
    public void handle(Request request) {
        decoratedHandler.handle(request);
    }
}

class CompressedRequestDecorator extends RequestDecorator {
    public CompressedRequestDecorator(HandlerChain decoratedHandler) {
        super(decoratedHandler);
    }

    @Override
    public void handle(Request request) {
        System.out.println("Request compressed for: " + request.getData());
        super.handle(request);
    }
}

class EncryptedRequestDecorator extends RequestDecorator {
    public EncryptedRequestDecorator(HandlerChain decoratedHandler) {
        super(decoratedHandler);
    }

    @Override
    public void handle(Request request) {
        System.out.println("Request encrypted for: " + request.getData());
        super.handle(request);
    }
}

// Request class
class Request {
    private String data;

    public Request(String data) {
        this.data = data;
    }

    public String getData() {
        return data;
    }
}

public class Main {
    public static void main(String[] args) {
        Server server = Server.getInstance();

        Request request1 = new Request("Sample request data 1");
        Request request2 = new Request("Sample request data 2");
        Request request3 = new Request("Sample request data 3");

        // запит з використанням динамічних декораторів
        HandlerChain handlerChain1 = new AuthenticationHandler();
        handlerChain1.addHandler(new CompressedRequestDecorator(new LoggingHandler()));
        handlerChain1.addHandler(new EncryptedRequestDecorator(new RequestHandler()));
        handlerChain1.handle(request1);

        // запит з іншими декораторами
        HandlerChain handlerChain2 = new AuthenticationHandler();
        handlerChain2.addHandler(new LoggingHandler());
        handlerChain2.addHandler(new EncryptedRequestDecorator(new RequestHandler()));
        handlerChain2.handle(request2);

        // запит без декораторів
        HandlerChain handlerChain3 = new AuthenticationHandler();
        handlerChain3.addHandler(new LoggingHandler());
        handlerChain3.addHandler(new RequestHandler());
        handlerChain3.handle(request3);
    }
}
