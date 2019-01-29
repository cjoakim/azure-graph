'use strict';

// Node.js Utility Program to invoke Azure Graph
// Chris Joakim, Microsoft, 2019/01/28
//
// Example command-line use:
// $ node main.js me

const fs = require("fs");
const request = require('request');
const MicrosoftGraph = require("@microsoft/microsoft-graph-client").Client;

class Main {

    constructor() {
        this.funct        = process.argv[2];
        this.base_url     = process.env.AZURE_APIM_URL;
        this.access_token = process.env.AZURE_APIM_ACCESS_TOKEN;

        console.log('access_token: ' + this.access_token);

        this.client = MicrosoftGraph.init({
            defaultVersion: 'v1.0',
            debugLogging: true,
            authProvider: (done) => {
                console.log('authProvider(done): ' + done);
                done(null, this.access_token);
            }
        });

        setTimeout(this.execute, 3000);
    }

    execute() {
        console.log('funct: ' + this.funct);
        switch (this.funct) {
            case 'me':
                this.me();
                break;
            default:
                console.log('error: unknown function - ' + this.funct);
        }
    }

    me() {
        console.log('function: me');
    }
}

new Main().execute();
