import * as vscode from "vscode";
import { exec } from "child_process";

function invokeFormatter(documentContents: string): Promise<string> {
  // invoke csharpier binary
  return new Promise((resolve, reject) => {
    const child = exec("dotnet csharpier", (error, stdout, stderr) => {
      if (error) {
        console.error(`exec error: ${error}`);
        reject(error);
      }
      resolve(stdout);
    });
    child.stdin!.write(documentContents);
    child.stdin!.end();
  });
}

export function activate(context: vscode.ExtensionContext) {
  let disposable = vscode.languages.registerDocumentFormattingEditProvider(
    { language: "csharp" },
    {
      async provideDocumentFormattingEdits(document: vscode.TextDocument) {
        const formattedText = await invokeFormatter(document.getText());
        // Replace entire document with formatted text
        return [
          vscode.TextEdit.replace(
            new vscode.Range(
              0,
              0,
              document.lineCount,
              document.lineAt(document.lineCount - 1).text.length
            ),
            formattedText
          ),
        ];
      },
    }
  );

  context.subscriptions.push(disposable);
}

// this method is called when your extension is deactivated
export function deactivate() {}
