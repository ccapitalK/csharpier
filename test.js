const { spawn } = require("child_process");
const fs = require("fs");

(async() => {
    const callbacks = [];
    
    const longRunner = spawn("LongRunner\\bin\\Debug\\net5.0\\LongRunner.exe", [], {
        stdio: "pipe"
    });

    longRunner.stdout.on("data", (chunk) => {
        console.log(chunk.toString());
        const callback = callbacks.shift();
        if (callback) {
            callback();
        }
    });

    function sendMessage(fileName, content) {
        longRunner.stdin.write(fileName);
        longRunner.stdin.write("\u0004");
        longRunner.stdin.write(content);
        longRunner.stdin.write("\u0004");
        return new Promise((resolve) => {
            callbacks.push(resolve);
        });
    }
    
    
    const files = fs.readdirSync("C:\\projects\\csharpier\\Src\\CSharpier.Tests\\FormattingTests\\TestFiles");
    for (let x = 0; x < files.length; x++) {
        let fileName = "C:\\projects\\csharpier\\Src\\CSharpier.Tests\\FormattingTests\\TestFiles\\" + files[x];
        const content = fs.readFileSync(fileName, { encoding: 'utf8' });
        await sendMessage(fileName, content);
    }

    longRunner.stdin.pause();
    longRunner.kill();
})();