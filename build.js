import fs from 'node:fs';
import process from 'node:process';
import pug from 'pug';

import metadata from './metadata.json' assert { type: "json" };

const env = process.argv[2] || 'dev';
console.log('Building for environment %s', env);

const data = {
	...metadata["default"],
	...(metadata[env] || {})
}

fs.readdir('Pug', (error, files) => {
	for (const file of files) {
		if (file.endsWith('.pug')) {
			const name = file.slice(0, Math.max(0, file.indexOf('.')));
			let xmlName = name + '/' + name + '.xml';
			if (name === 'csproj') {
				xmlName = data.assemblyName + '.csproj';
			} else if (name == 'loadFolders') {
				xmlName = name + '.xml';
			} else if (name === 'html') {
				xmlName = data.assemblyName + '.html';
			} else {
				fs.mkdir(name, _ => { });
			}

			console.log('Rendering ' + name);
			const renderer = pug.compileFile('Pug/' + file, { pretty: true });
			fs.writeFile(xmlName, renderer(data), error_ => {
				if (error_) {
					console.error(error_);
				}
			});
		}
	}
});