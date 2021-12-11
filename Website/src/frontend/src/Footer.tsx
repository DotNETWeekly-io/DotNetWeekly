import React from 'react';
import styles from './Footer.module.css';

export class Footer extends React.Component {
    render() {
        return (
            <div className={styles.footer}>
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
            </div>
        );
    }
}

export default Footer;
