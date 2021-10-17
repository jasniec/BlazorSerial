var blazorSerialPort;

export function blazorSerialIsSupported() {
    return navigator.serial ? true : false;
}

export async function blazorSerialGetPort() {
    try {
        blazorSerialPort = await navigator.serial.requestPort();
        return "Ok";
    }
    catch (ex) {
        if (ex instanceof SecurityError) {
            return "SecurityError";
        }
        else if (ex instanceof AbortError) {
            return "AbortError";
        }
        else {
            return "Unknown";
        }
    }
}

export async function blazorSerialConnect(baudRate) {
    try {
        await blazorSerialPort.open({ baudRate: baudRate });
        return "Ok";
    }
    catch (ex) {
        if (ex instanceof InvalidStateError) {
            return "InvalidStateError";
        }
        else if (ex instanceof NetworkError) {
            return "NetworkError";
        }
        else {
            return "Unknown";
        }
    }
}

export function blazorSerialWriteText(text) {
    let writer = port.writable.getWriter();
    writer.write(textEncoder.encode(text));
    writer.releaseLock();
}