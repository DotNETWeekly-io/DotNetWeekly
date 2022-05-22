import React from 'react';
import { Stack } from '@fluentui/react';

export class Footer extends React.Component {
    render() {
        return (
            <Stack horizontal horizontalAlign="center" verticalAlign="baseline" styles={{
                root: {
                    left: 0,
                    bottom: 0,
                    width: "100%",
                    height: "30px",
                    position: "fixed",
                    "background-color": "#f7f8fa",
                    "text-align": "center",
                    "font-size": "12px",
                    "font-style": "italic",
                }
            }}>
                <p>
                    本周刊开源，(GitHub:
                    <a
                        href="https://github.com/gaufung/DotNetWeekly"
                        target="_blank"
                        rel="noreferrer"
                    >
                        gaufung/DotNetWeekly
                    </a>
                    )，欢迎提交 issue 投稿。
                </p>
            </Stack>
        );
    }
}

export default Footer;
