import React from 'react';
import { Image, Label, Stack, StackItem } from '@fluentui/react';
import dotnet from './dotnet-bot.svg';

export class Header extends React.Component {
    render() {
        return (
            <Stack
                horizontal
                horizontalAlign="space-between"
                verticalAlign="center"
                styles={{
                    root: {
                        height: '40px',
                        margin: '1px',
                        'background-color': 'rgb(243, 237, 229)',
                        'font-weight': 'bold',
                    },
                }}
            >
                <StackItem>
                    <a href={window.origin}>
                        <img
                            src={dotnet}
                            alt="Logo"
                            width="35px"
                            style={{ marginLeft: '10px' }}
                        />
                    </a>
                </StackItem>
                <StackItem>
                    <Label>.NET 周刊</Label>
                </StackItem>
                <StackItem>
                    <span></span>
                </StackItem>
            </Stack>
        );
    }
}

export default Header;
